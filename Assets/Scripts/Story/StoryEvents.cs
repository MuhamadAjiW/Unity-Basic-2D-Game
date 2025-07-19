// Nested classes for strongly-typed story events
public class StoryEvents
{
  public Chapter1 chapter1 { get; private set; }
  public Chapter2 chapter2 { get; private set; }

  public StoryEvents()
  {
    chapter1 = new Chapter1();
    chapter2 = new Chapter2();
  }
}

public class Chapter1
{
  public bool EventA { get; set; } = false;
  public bool EventB { get; set; } = false;
}

public class Chapter2
{
  public bool EventC { get; set; } = false;
  public bool EventD { get; set; } = false;
}