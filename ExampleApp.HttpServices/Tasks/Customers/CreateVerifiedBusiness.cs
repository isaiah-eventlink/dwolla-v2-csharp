﻿using Dwolla.Client.Models;
using Dwolla.Client.Models.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleApp.HttpServices.Tasks.Customers
{
    [Task("cvbc", "Create a Verified Business Customer")]
    internal class CreateVerifiedBusiness : BaseTask
    {
        public override async Task Run()
        {
            await HttpService.Customers.CreateCustomerAsync(
                new CreateCustomerRequest
                {
                    FirstName = "authorized",
                    LastName = "rep",
                    Email = $"{RandomAlphaNumericString(20)}@example.com",
                    Type = "business",
                    Address1 = "123 Sesame St",
                    City = "SmallTown",
                    State = "VA",
                    PostalCode = "22222",
                    Phone = "1234567890",
                    BusinessName = "Test Business",
                    BusinessType = "corporation",
                    BusinessClassification = "9ed3f67a-7d6f-11e3-bb5b-5404a6144203",
                    Ein = RandomNumericString(9),
                    Controller = new Controller()
                    {
                        FirstName = "the",
                        LastName = "controller",
                        Title = "big cheese",
                        Address = new Address()
                        {
                            Address1 = "000 Awesome Ave",
                            City = "Big Town",
                            StateProvinceRegion = "VA",
                            PostalCode = "22222",
                            Country = "US"
                        },
                        DateOfBirth = new DateTime(1980, 1, 1),
                        Ssn = RandomNumericString(4)
                    }
                });

            var response = await HttpService.Customers.GetCustomerCollectionAsync(string.Empty, string.Empty, new List<string>(), null, null);

            response.Content.Embedded.Customers
                .ForEach(c => WriteLine($"Customer: {c.Id} - {c.FirstName} - {c.LastName}"));
        }
    }
}