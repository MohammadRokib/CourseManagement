﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Entities {
    public class Teacher : User {
        public List<Course> AssignedCourses { get; set; }
    }
}
