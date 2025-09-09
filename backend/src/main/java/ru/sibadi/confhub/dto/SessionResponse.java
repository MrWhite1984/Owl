package ru.sibadi.confhub.dto;

public class SessionResponse {
    private String sessionToken;

    public SessionResponse(String sessionToken) {
        this.sessionToken = sessionToken;
    }

    public String getSessionToken() {
        return sessionToken;
    }
}
