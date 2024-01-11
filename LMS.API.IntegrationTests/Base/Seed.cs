using Infrastructure;
using LMS.Domain.Entities;

namespace LMS.API.IntegrationTests.Base;

public class Seed
{
    public static void InitializeDbForTests(GlobalLMSContext context)
    {
        var courses = new List<Course>
        {
            Course.Create("Course 1", Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")).Value,
            Course.Create("Course 2", Guid.Parse("6d95df85-c2c8-4967-acf2-e470e85e5fa2")).Value
        };
        
        var lectures = new List<Lecture>
        {
            Lecture.Create(Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd"), "Lecture 1", "da.com").Value,
            Lecture.Create(Guid.Parse("6d95df85-c2c8-4967-acf2-e470e85e5fa2"), "Lecture 2", "da.com").Value
        };
        
        var steps = new List<Step>
        {
            Step.Create(Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")).Value,
            Step.Create(Guid.Parse("6d95df85-c2c8-4967-acf2-e470e85e5fa2")).Value
        };
        
        context.Lectures.RemoveRange();
        context.Lectures.AddRange(lectures);
        
        context.Steps.RemoveRange();
        context.Steps.AddRange(steps);
        
        context.Courses.RemoveRange();
        context.Courses.AddRange(courses);
        
        context.SaveChanges();
        
        var quizzes = new List<Quiz>
        {
            Quiz.Create("Quiz 1", Guid.Parse("aedefc40-7093-4118-abc3-b2f0929353fd")).Value,
            Quiz.Create("Quiz 2", Guid.Parse("6d95df85-c2c8-4967-acf2-e470e85e5fa2")).Value
        };
        context.Quizes.RemoveRange();
        context.Quizes.AddRange(quizzes);
        context.SaveChanges();
    } 
}