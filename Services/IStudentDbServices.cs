using Cw5.DTOs.Requests;
using Cw5.DTOs.Responses;
using Cw5.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw5.Services
{
    public interface IStudentDbServices
    {
        Enrollment EnrollStudent(EnrollStudentRequest request);
        // void PromoteStudents(string name,int semester);
        PromoteStudentRequest PromoteStudents(PromoteStudentRequest request);
    }
}
