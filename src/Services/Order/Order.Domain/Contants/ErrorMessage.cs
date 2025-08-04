using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Contants
{
    public class ErrorMessage
    {
        #region OrderBillingDetail
        public static readonly string FirstNameIsRequired = "First name is required.";
        public static readonly string LastNameIsRequired = "Last name is required.";
        public static readonly string EMailNotValid = "E-Mail is not valid.";
        public static readonly string AddresslineRequired = "AddressLine is required.";
        public static readonly string CountryRequired = "Country is required.";
        public static readonly string StateRequired = "State is required.";
        public static readonly string ZipcodeRequired = "Zipcode is required.";
        #endregion

        #region Order
        public static readonly string UserIdRequired = "UserId is required.";
        public static readonly string InvalidOrderStatus = "Invalid order status.";
        public static readonly string TotalPriceMustBeGreaterThanZero = "Total price must be greater than zero.";
        public static readonly string BillingDetailIsRequired = "Billing detail is required.";



        #region
    }
}
