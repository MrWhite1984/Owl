package ru.sibadi.confhub.dto;

public class PeopleLightResponse {
    private String surname;
    private String name;
    private boolean isVerified;
    private String profilePhoto;

    public PeopleLightResponse(String surname, String name, boolean isVerified, String profilePhoto) {
        this.surname = surname;
        this.name = name;
        this.isVerified = isVerified;
        this.profilePhoto = profilePhoto;
    }
    
}
