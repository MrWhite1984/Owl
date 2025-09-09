package ru.sibadi.confhub.controller;

import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpSession;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.HttpStatusCode;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ru.sibadi.confhub.dto.LoginRequest;
import ru.sibadi.confhub.dto.PeopleRequest;
import ru.sibadi.confhub.dto.SessionResponse;
import ru.sibadi.confhub.entity.People;
import ru.sibadi.confhub.service.PeopleServices;
import ru.sibadi.confhub.service.RedisSessionService;
import ru.sibadi.confhub.service.RoleServices;

@RestController
@RequestMapping("/api/people/")
public class PeopleController {

    @Autowired
    private PeopleServices peopleServices;

    @Autowired
    private RoleServices roleServices;

    @Autowired
    private RedisSessionService redisSessionService;



    @PostMapping("/registration")
    public ResponseEntity<People> createPeople(@RequestBody PeopleRequest request){
        People people = new People(
                request.getSurname(),
                request.getName(),
                request.getPatronymic(),
                request.getEducationalInstitution(),
                request.getJobTitle(),
                request.getCity(),
                request.getPhone(),
                request.getEmail(),
                request.getPassword(),
                roleServices.getRolesByTitles(request.getRoles())
        );
        People savedPeople = peopleServices.createPeople(people);
        return ResponseEntity.ok(savedPeople);
    }

    @PostMapping("/login")
    public ResponseEntity<?> login(@RequestBody LoginRequest loginRequest, HttpServletRequest request){
        People people = peopleServices.authenticate(loginRequest.getEmail(), loginRequest.getPassword());
        if(people == null)
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body("Invalid email or password");
        else{
            String sessionToken = redisSessionService.createSession(people.getId());
            return ResponseEntity.ok(new SessionResponse(sessionToken));
        }
    }

}
