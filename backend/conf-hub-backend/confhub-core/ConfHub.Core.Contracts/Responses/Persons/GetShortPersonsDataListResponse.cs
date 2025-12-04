namespace ConfHub.Core.Contracts.Responses.Persons
{
    public record GetShortPersonsDataListResponse
        (
            IEnumerable<GetShortPersonDataResponse> ShortPersonsDataList
        );
}
