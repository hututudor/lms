﻿using LMS.Domain.Common;

namespace LMS.Application.Persistence;

public interface IAsyncRepository<T> where T: class
{
    Task<Result<T>> AddAsync(T entity);
    Task<Result<T>> UpdateAsync(T entity);
    Task<Result<T>> DeleteAsync(Guid id);
    Task<Result<T>> FindByIdAsync(Guid id);
    Task<Result<IReadOnlyList<T>>> GetAll();
}