namespace Exam_Final_Project.controllers;

public enum ControllerStatus
{
    Forbidden
}

public class ControllerException : Exception
{
    public ControllerStatus Status { get; set; }

    public ControllerException(string? message, ControllerStatus controllerStatus) : base(message)
    {
        Status = controllerStatus;
    }
}