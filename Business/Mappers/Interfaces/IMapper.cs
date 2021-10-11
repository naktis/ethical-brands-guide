namespace Business.Mappers.Interfaces
{
    public interface IMapper<TEntity, TInDto>
        where TEntity : class
        where TInDto : class
    {
        public TEntity EntityFromDto(TInDto dto);
    }
}
