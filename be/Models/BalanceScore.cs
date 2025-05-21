using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.Models
{
    public class BalanceScore
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public double WhatScale { get; set; } = 0;
        public double HowScale { get; set; } = 0;
        public Guid? PositionId { get; set; }
        public Position? Position { get; set; }
    }
}