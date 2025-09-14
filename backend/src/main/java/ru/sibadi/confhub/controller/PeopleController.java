package ru.sibadi.confhub.controller;

import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpSession;
import org.junit.platform.commons.logging.Logger;
import org.junit.platform.commons.logging.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.HttpStatusCode;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ru.sibadi.confhub.dto.*;
import ru.sibadi.confhub.entity.People;
import ru.sibadi.confhub.exceptions.EmailAlreadyExistsException;
import ru.sibadi.confhub.service.PeopleServices;
import ru.sibadi.confhub.service.RedisSessionService;
import ru.sibadi.confhub.service.RoleServices;

import java.util.Map;

@RestController
@RequestMapping("/api/people/")
public class PeopleController {

    @Autowired
    private PeopleServices peopleServices;

    @Autowired
    private RoleServices roleServices;

    @Autowired
    private RedisSessionService redisSessionService;

    private static final Logger log = LoggerFactory.getLogger(PeopleController.class);

    @PostMapping("/registration")
    public ResponseEntity<?> createPeople(@RequestBody PeopleRequest request){
        log.info(()->"Invoked Registration");
        try{
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
                    roleServices.getRolesByTitles(request.getRoles()),
                    request.geteLibLink()
            );
            People savedPeople = peopleServices.createPeople(people);
            var token = redisSessionService.createSession(savedPeople.getId());
            return ResponseEntity.ok(new SessionResponse(token));
        }
        catch (EmailAlreadyExistsException e){
            return ResponseEntity.status(HttpStatus.CONFLICT).body(Map.of("errMessage", e.getMessage()));
        }
        catch (Exception e){
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(Map.of("errMessage", e.getMessage()));
        }

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

    @PostMapping("/getRoles")
    public ResponseEntity<?> getRoles(@RequestBody TokenRequest request){
        if(redisSessionService.getUserIdBySessionToken(request.getToken()) == null)
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body("The token is expired or does not exist");
        People people = peopleServices.getPeopleById(redisSessionService.getUserIdBySessionToken(request.getToken()));
        return ResponseEntity.ok(people.getRoles());
    }

    @PostMapping("/getPeopleLight")
    public ResponseEntity<?> getPeopleLight(@RequestBody TokenRequest request){
        if(redisSessionService.getUserIdBySessionToken(request.getToken()) == null)
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body("The token is expired or does not exist");
        People people = peopleServices.getPeopleById(redisSessionService.getUserIdBySessionToken(request.getToken()));
        return ResponseEntity.ok(new PeopleLightResponse(people.getSurname(), people.getName(), people.isVerified(), people.getProfilePhoto()));
    }
}
