using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public Coupon? Coupon { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context
                    .Users
                    .FirstOrDefaultAsync(entity => entity.UserName == _userAccessor.GetUsername())
                ;

            Coupon coupon = null;
            if (request.Coupon != null)
            {
                coupon = await _context.Coupons
                    .FirstOrDefaultAsync(entity => entity.Id == request.Coupon.Id);

                if (!coupon.Users.Any(entity => entity.User.Id == user.Id))
                    return Result<Unit>.Failure("You are not eligible for using this coupon");
            }

            var cartDetailsList = await _context
                    .CartDetails
                    .Where(entity => entity.CartId == user.Id)
                    .Include(entity => entity.Product)
                    .ThenInclude(entity => entity.Prices)
                    .Include(entity => entity.VariationOption)
                    .ToListAsync()
                ;
            var orderDetailsList = new List<OrderDetails>();
            foreach (var cartDetails in cartDetailsList)
            {
                var discount = await _context
                    .DiscountProducts
                    .Where(entity =>
                        entity.Discount.DiscountStatus == DiscountStatus.OnGoing &&
                        entity.ProductId == cartDetails.ProductId)
                    .OrderByDescending(entity => entity.Discount.Amount)
                    .FirstAsync();
                var price = cartDetails.Product.Prices.OrderByDescending(entity => entity.Amount).First();
                var orderDetails = new OrderDetails
                {
                    Product = cartDetails.Product,
                    Quantity = cartDetails.Quantity,
                    Price = price,
                    Discount = discount.Discount.Amount,
                    VariationOption = cartDetails.VariationOption,
                    Total = price.Amount - discount.Discount.Amount
                };
                orderDetailsList.Add(orderDetails);
            }

            var order = new CustomerOrder
            {
                OrderDetails = orderDetailsList,
                Coupon = coupon,
                OrderDate = DateTime.Now,
                User = user,
                Status = OrderStatus.Pending
            };

            _context.CustomerOrders.Add(order);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to save changes");

            _context
                .CartDetails
                .RemoveRange(cartDetailsList);

            result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to save changes");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}