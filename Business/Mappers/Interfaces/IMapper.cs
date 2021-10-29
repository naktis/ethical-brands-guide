namespace Business.Mappers.Interfaces
{
    public interface IMapper<TEntity, TInDto, TOutDto>
        where TEntity : class
        where TInDto : class
        where TOutDto : class
    {
        public TOutDto EntityToDto(TEntity entity);
        public TEntity EntityFromDto(TInDto dto);
        public TEntity CopyFromDto(TEntity entity, TInDto dto);
    }
}
