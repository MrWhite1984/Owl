package ru.sibadi.confhub.dto;

public class PeopleLightResponse {
    private String surname;
    private String name;
    private boolean isVerified;
    private String profilePhoto;
    private String jobTitle;

    public PeopleLightResponse(String surname, String name, boolean isVerified, String profilePhoto, String jobTitle) {
        this.surname = surname;
        this.name = name;
        this.isVerified = isVerified;
        this.profilePhoto = profilePhoto;
        this.jobTitle = jobTitle;
    }

    public String getSurname() {
        return surname;
    }

    public String getName() {
        return name;
    }

    public boolean isVerified() {
        return isVerified;
    }

    public String getProfilePhoto() {
        return profilePhoto;
    }

    public String getJobTitle() {
        return jobTitle;
    }
}
