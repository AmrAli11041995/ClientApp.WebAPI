﻿namespace Customer.DTOs.AppDTOs.Client
{
    public class ClientListingDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set;}  
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
