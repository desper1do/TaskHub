using System;

namespace Api.DiTest.Interfaces;

public interface IHasInstanceId
{
    Guid InstanceId { get; }
}