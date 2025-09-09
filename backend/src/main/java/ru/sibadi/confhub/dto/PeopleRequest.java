package ru.sibadi.confhub.dto;

import java.util.Set;

public class PeopleRequest {
    private String surname;
    private String name;
    private String patronymic;
    private String educationalInstitution;
    private String jobTitle;
    private String city;
    private String phone;
    private String email;
    private String password;
    private Set<String> roles;

    public String getSurname() {
        return surname;
    }

    public String getName() {
        return name;
    }

    public String getPatronymic() {
        return patronymic;
    }

    public String getEducationalInstitution() {
        return educationalInstitution;
    }

    public String getJobTitle() {
        return jobTitle;
    }

    public String getCity() {
        return city;
    }

    public String getPhone() {
        return phone;
    }

    public String getEmail() {
        return email;
    }

    public String getPassword() {
        return password;
    }

    public Set<String> getRoles() {
        return roles;
    }
}
