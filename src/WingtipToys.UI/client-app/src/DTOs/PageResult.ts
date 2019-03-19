export default interface PageResult<T> {
    pageNo: number;
    pageSize: number;
    totalPages: number;
    totalCount: number;
    items: T[];
}