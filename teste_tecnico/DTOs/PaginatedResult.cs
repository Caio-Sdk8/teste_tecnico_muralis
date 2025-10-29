namespace teste_tecnico.DTOs
{
    public record PaginatedResult<T>(List<T> Items, int Total, int Page, int PageSize)
    {
        public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);
    }
}
