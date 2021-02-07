public class EventBus
{
    /// <summary>
    /// реестр для всех объектов
    /// </summary>
    public static EventBus Bus = new EventBus();

    /// <summary>
    /// добавить в список уведомлений. сигнатура добавляемого объекта
    /// </summary>
    /// <param name="objectWithTitle">параметр типа</param>
    public delegate void FunctionForObjectWithTitle(ObjectWithTitle objectWithTitle);
         
    //Список, в который можно добавлять функции.
    public event FunctionForObjectWithTitle ForObjectWithNameChange;

    /// <summary>
    /// Вызывает все функции в списке событий ForObjectWithNameChange
    /// </summary>
    /// <param name="objectWithTitle">Объект, который должен быть передан функциям в списке событий </param>
    /// 

    public void CallToChangeNameOfObject(ObjectWithTitle objectWithTitle)
    {
        ForObjectWithNameChange(objectWithTitle);
    }
}
