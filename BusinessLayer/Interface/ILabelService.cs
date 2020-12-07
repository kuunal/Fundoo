﻿using ModelLayer;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelService
    {
        Task<Label> AddLabelAsync(int userId, Label label);
    }
}
