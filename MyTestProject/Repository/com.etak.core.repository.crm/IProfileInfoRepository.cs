﻿using System;

using com.etak.core.model;

namespace com.etak.core.repository.crm
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProfileInfoRepository<T> : IRepository<T, Int64> where T : ProfileInfo
    {
    }
}