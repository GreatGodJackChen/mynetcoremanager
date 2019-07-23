using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Data.BaseEntity
{
    public interface IEntity:IEntity<string>
    {
    }
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        TPrimaryKey Id { get; set; }

        bool IsTransient();
    }
}
