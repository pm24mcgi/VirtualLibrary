﻿namespace VirtualLibrary.Model
{
    public class Checkout
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
    }
}
