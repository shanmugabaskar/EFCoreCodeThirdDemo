﻿// See https://aka.ms/new-console-template for more information

// Initialize the database context
using EFCoreCodeSecondDemo;
using Microsoft.EntityFrameworkCore;

//Eager Loading in Entity Framework Core
using (var context = new EFCoreDbContext())
{
    try
    {
        //Sample 1
        // Method Syntax with Include (using lambda expression) 
        Console.WriteLine("Method Syntax: Loading Students and their Addresses\n");
        // Eagerly load Student entities along with their related Address entities using method syntax.
        var studentsWithAddressesMethod = context.Students
            .Include(s => s.Address) // Eager load Address entity using a lambda expression
            .ToList();
        // Method Syntax with Include (using string parameter)
        // Eagerly load Student entities along with their related Address entities using string-based Include.
        var studentsWithAddressesMethodString = context.Students
            .Include("Address") // Eager load Address entity using string parameter
            .ToList();
        // Eager Loading using Query Syntax with Lambda Expression
        //var studentsWithAddressesQueryLambda = (from student in context.Students
        //                                        .Include(s => s.Address) // Eagerly load Address entity using lambda in query syntax
        //                                        select student).ToList();
        // Eager Loading using Query Syntax with String
        //var studentsWithAddressesQueryString = (from student in context.Students
        //                                        .Include("Address") // Eagerly load Address entity using string in query syntax
        //                                        select student).ToList();
        // Display results
        Console.WriteLine(); // Display a new line before displaying the data
        foreach (var student in studentsWithAddressesMethod)
        {
            if (student.Address != null)
            {
                // Address exists, display the full address details
                Console.WriteLine($"Student: {student.FirstName} {student.LastName}, Address: {student.Address.Street}, {student.Address.City}, {student.Address.State}");
            }
            else
            {
                // Address is null, display "No Address"
                Console.WriteLine($"Student: {student.FirstName} {student.LastName}, Address: No Address");
            }
        }

        //Sample 2
        // METHOD SYNTAX with Include (using lambda expression)
        Console.WriteLine("Method Syntax: Loading Students with Branch, Address, and Courses");
        // Eagerly load Student entities along with their related Branch, Address, and Courses entities using method syntax.
        var studentsWithDetailsMethod = context.Students
            .Include(s => s.Branch)            // Eagerly load Branch entity
            .Include(s => s.Address)           // Eagerly load Address entity
            .Include(s => s.Courses)           // Eagerly load Courses collection
            .ToList();
        Console.WriteLine(); //Line Break before displaying the data
                             // Display results
        foreach (var student in studentsWithDetailsMethod)
        {
            Console.WriteLine($"Student: {student.FirstName} {student.LastName}, Branch: {student.Branch?.BranchLocation}, " +
                $"Address: {(student.Address == null ? "No Address" : student.Address.City)}, Courses Count: {student.Courses.Count}");
        }
        // QUERY SYNTAX with Include (using lambda expression)
        // Console.WriteLine("\nQuery Syntax: Loading Students with Branch, Address, and Courses");
        // Eagerly load Student entities along with their related Branch, Address, and Courses entities using query syntax.
        //var studentsWithDetailsQuery = (from student in context.Students
        //                                .Include(s => s.Branch)             // Eagerly load Branch entity
        //                                .Include(s => s.Address)            // Eagerly load Address entity
        //                                .Include(s => s.Courses)            // Eagerly load Courses collection
        //                                select student).ToList();
        // Display results
        //foreach (var student in studentsWithDetailsQuery)
        //{
        //    Console.WriteLine($"Student: {student.FirstName} {student.LastName}, Branch: {student.Branch?.BranchLocation}, " +
        //        $"Address: {(student.Address == null ? "No Address" : student.Address.City)}, Courses Count: {student.Courses.Count}");
        //}

        //Sample 3
        // Method Syntax with Include and ThenInclude (using lambda expressions)
        Console.WriteLine("Loading Students and their related entities\n");
        // Eagerly load Student, Branch, Address, Courses, and the related Subjects using method syntax
        var student1 = (context.Students
            .Where(std => std.StudentId == 1)
            .Include(s => s.Branch)               // Eagerly load related Branch
            .Include(s => s.Address)              // Eagerly load related Address
            .Include(s => s.Courses)              // Eagerly load related Courses
            .ThenInclude(c => c.Subjects))        // Eagerly load related Subjects for each Course
            .FirstOrDefault();                    // Execute the query and retrieve the data
                                                  // Display basic student information
        Console.WriteLine($"Student: {student1.FirstName} {student1.LastName}");
        Console.WriteLine($"Branch: {student1.Branch?.BranchLocation}");
        Console.WriteLine($"Address: {student1.Address?.Street}, {student1.Address?.City}, {student1.Address?.State}");
        // Display each course and its related subjects
        foreach (var course in student1.Courses)
        {
            Console.WriteLine($"Course: {course.Name}");
            foreach (var subject in course.Subjects)
            {
                Console.WriteLine($"    Subject: {subject.SubjectName}");
            }
        }
    }
    catch (Exception ex)
    {
        // Handle exceptions
        Console.WriteLine($"An error occurred while fetching the data. Error: {ex.Message}");
    }
}
// Final Output
Console.WriteLine("Eager loading completed.");



Console.ReadKey();