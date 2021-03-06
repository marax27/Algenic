﻿using System.Collections.Generic;

namespace Algenic.ViewModels
{
    public class ContestSolutionAggregate
    {
        public IDictionary<string, UserSolutionAggregate> Users { get; } = new Dictionary<string, UserSolutionAggregate>();
    }

    public class UserSolutionAggregate
    {
        public string UserName { get; }
        public ISet<TaskDto> Tasks { get; } = new HashSet<TaskDto>();

        public UserSolutionAggregate(string userName)
            => UserName = userName;
    }

    public class TaskDto
    {
        public int Id { get; }
        public string Name { get; }

        public TaskDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
