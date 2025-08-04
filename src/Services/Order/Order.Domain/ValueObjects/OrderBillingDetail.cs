using Order.Domain.Common;
using Order.Domain.Contants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.ValueObjects
{
    public class OrderBillingDetail : ValueObject
    {
        private OrderBillingDetail(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public string AddressLine { get; }
        public string Country { get; }
        public string State { get; }
        public string ZipCode { get; }

        public static Result<OrderBillingDetail> Create(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result<OrderBillingDetail>.Failure(new(ErrorMessage.FirstNameIsRequired,"Order.Domain.ValueObjects.OrderBillingDetail - Create"));

            if (string.IsNullOrWhiteSpace(lastName))
                return Result<OrderBillingDetail>.Failure(new(ErrorMessage.FirstNameIsRequired, "Order.Domain.ValueObjects.OrderBillingDetail - Create"));

            if (string.IsNullOrWhiteSpace(emailAddress))
                return Result<OrderBillingDetail>.Failure(new(ErrorMessage.LastNameIsRequired, "Order.Domain.ValueObjects.OrderBillingDetail - Create"));

            if (string.IsNullOrWhiteSpace(addressLine))
                return Result<OrderBillingDetail>.Failure(new(ErrorMessage.AddresslineRequired, "Order.Domain.ValueObjects.OrderBillingDetail - Create"));

            if (string.IsNullOrWhiteSpace(country))
                return Result<OrderBillingDetail>.Failure(new(ErrorMessage.CountryRequired, "Order.Domain.ValueObjects.OrderBillingDetail - Create"));

            if (string.IsNullOrWhiteSpace(state))
                return Result<OrderBillingDetail>.Failure(new(ErrorMessage.StateRequired, "Order.Domain.ValueObjects.OrderBillingDetail - Create"));

            if (string.IsNullOrWhiteSpace(zipCode))
                return Result<OrderBillingDetail>.Failure(new(ErrorMessage.ZipcodeRequired, "Order.Domain.ValueObjects.OrderBillingDetail - Create"));

            return Result<OrderBillingDetail>.Success(new (firstName, lastName, emailAddress, addressLine, country, state, zipCode));
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return EmailAddress;
            yield return AddressLine;
            yield return Country;
            yield return State;
            yield return ZipCode;
        }
    }
}
