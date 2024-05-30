using Omu.ValueInjecter;

namespace Core.ObjectMapper.Abstract.Profiles;

public abstract class BaseMapperProfile<T, TV> where TV : new()
{
    protected void Bind(Action<T, TV>? complexMappingAction = null)
    {
        Mapper.AddMap<T, TV>(t =>
        {
            var tv = new TV();
            tv.InjectFrom(t);

            complexMappingAction?.Invoke(t, tv);

            return tv;
        });
    }
}