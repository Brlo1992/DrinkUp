namespace DrinkUp.WebApi.Model.Service {
    public enum Status {
        OperationFailed = 0,
        Added,
        Removed,
        Updated,
        OneSelected,
        ManySelected,
        AlreadyExist,
        NotExist,
        FindMoreThanOne
    }
}