package ru.sibadi.confhub.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import ru.sibadi.confhub.dto.TokenRequest;
import ru.sibadi.confhub.service.RedisSessionService;

@RestController
@RequestMapping("/api/token/")
public class TokenController {

    @Autowired
    private RedisSessionService redisSessionService;

    @PostMapping("/validate")
    public ResponseEntity<?> validateToken(@RequestBody TokenRequest request){
        if(redisSessionService.getUserIdBySessionToken(request.getToken()) != null)
            return ResponseEntity.ok(true);
        else
            return ResponseEntity.ok(false);
    }
}
