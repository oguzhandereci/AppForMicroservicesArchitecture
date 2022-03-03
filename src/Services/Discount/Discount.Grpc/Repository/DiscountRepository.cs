using Discount.Grpc.Data;
using Discount.Grpc.Entities;
using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Discount.Grpc.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private IDbConnection _dbConn;
        public DiscountRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _dbConn = dbConnectionFactory.GetOpenConnection();
        }

        public async Task<Coupon> GetCouponForDiscount(string productId)
        {
            const string _sqlCommand = @"
                                            SELECT 
                                                ""id"", 
                                                ""productid"", 
                                                ""description"",    
                                                ""amount""
	                                        FROM public.""coupon""
                                            WHERE ""productid""=@prdId;
                                        ";
            Coupon _coupon = await _dbConn.QueryFirstOrDefaultAsync<Coupon>(_sqlCommand, new { prdId = productId });
            return _coupon ?? throw new Exception("Cannot find any coupon with that productId");
        }

        public async Task<bool> InsertCoupon(Coupon coupon)
        {
            const string _sqlCommand = @"
                                            INSERT INTO public.""coupon""
                                                (""productid"", 
                                                 ""description"",    
                                                 ""amount"")
	                                        VALUES (@prdId, @desc, @amount);
                                        ";

            var affected = await _dbConn.ExecuteAsync(_sqlCommand, new { prdId = coupon.ProductId, desc = coupon.Description, amount = coupon.CouponAmount });

            return Convert.ToBoolean(affected);
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            const string _sqlCommand = @"
                                            UPDATE public.""coupon""
                                            SET ""productid"" = @prdId, 
                                                ""description"" = @desc,    
                                                ""amount"" = @amount
	                                        WHERE ""id""=@Id;
                                        ";

            var affected = await _dbConn.ExecuteAsync(_sqlCommand, new {Id = coupon.CouponId, prdId = coupon.ProductId, desc = coupon.Description, amount = coupon.CouponAmount });

            return Convert.ToBoolean(affected);
        }

        public async Task<bool> DeleteCoupon(string productId)
        {
            const string _sqlCommand = @"
                                            DELETE FROM public.""coupon""
	                                        WHERE ""productid""=@prdId;
                                        ";

            var affected = await _dbConn.ExecuteAsync(_sqlCommand, new { prdId = productId });

            return Convert.ToBoolean(affected);
        }
    }
}
