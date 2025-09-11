package ru.sibadi.confhub.entity;

import jakarta.persistence.*;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.AllArgsConstructor;

import java.util.HashSet;
import java.util.Set;
import java.util.UUID;

@Entity
@Table(name = "people")
public class People {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    @Column(updatable = false, nullable = false)
    private UUID id;

    @Column(updatable = true, nullable = false)
    private String surname;

    @Column(updatable = true, nullable = false)
    private String name;

    @Column(updatable = true, nullable = true)
    private String patronymic;

    @Column(name="educational_institution", updatable = true, nullable = false)
    private String educationalInstitution;

    @Column(name="job_title", updatable = true, nullable = false)
    private String jobTitle;

    @Column(updatable = true, nullable = false)
    private String city;

    @Column(updatable = true, nullable = false)
    private String phone;

    @Column(updatable = true, nullable = false, unique = true)
    private String email;

    @Column(updatable = true, nullable = false)
    private String password;

    @Column(name="is_verified", updatable = true, nullable = false)
    private boolean isVerified;

    @ManyToMany(fetch = FetchType.LAZY)
    @JoinTable(
            name = "users",
            joinColumns = @JoinColumn(name = "people_id"),
            inverseJoinColumns = @JoinColumn(name = "role_id")
    )
    private Set<Roles> roles = new HashSet<>();

    @ManyToMany(mappedBy = "participants", fetch = FetchType.LAZY)
    private Set<Projects> projects = new HashSet<>();

    @ManyToMany(mappedBy = "moderators", fetch = FetchType.LAZY)
    private Set<Projects> moderated_projects = new HashSet<>();

    @OneToMany (mappedBy = "author", fetch = FetchType.LAZY)
    private Set<News> news = new HashSet<>();

    @Column (name="profile_photo", updatable = true, nullable = true)
    private String profilePhoto;

    @Column (name="e_lib_link", updatable = true, nullable = true)
    private String eLibLink;

    public People(String surname,
                  String name,
                  String patronymic,
                  String educationalInstitution,
                  String jobTitle,
                  String city,
                  String phone,
                  String email,
                  String password,
                  Set<Roles> roles,
                  String eLibLink){
        this.surname = surname;
        this.name = name;
        this.patronymic = patronymic;
        this.educationalInstitution = educationalInstitution;
        this.jobTitle = jobTitle;
        this.city = city;
        this.phone = phone;
        this.email = email;
        this.password = password;
        this.roles = roles;
        this.eLibLink = eLibLink;
    }

    public People() {
    }

    public UUID getId() {
        return id;
    }

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

    public boolean isVerified() {
        return isVerified;
    }

    public Set<Roles> getRoles() {
        return roles;
    }

    public String getProfilePhoto() {
        return profilePhoto;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public void setProfilePhoto(String profilePhoto) {
        this.profilePhoto = profilePhoto;
    }

    public void setRoles(Set<Roles> roles) {
        this.roles = roles;
    }
}