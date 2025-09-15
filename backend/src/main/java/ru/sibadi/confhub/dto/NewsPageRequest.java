package ru.sibadi.confhub.dto;

public class NewsPageRequest {
    private int pageNumber;
    private int pageSize;

    public NewsPageRequest(int pageNumber, int pageSize) {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
    }

    public int getPageNumber() {
        return pageNumber;
    }

    public int getPageSize() {
        return pageSize;
    }
}
