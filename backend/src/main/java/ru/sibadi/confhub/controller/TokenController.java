package ru.sibadi.confhub.controller;

import org.junit.platform.commons.logging.Logger;
import org.junit.platform.commons.logging.LoggerFactory;
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

    private static final Logger log = LoggerFactory.getLogger(TokenController.class);

    @PostMapping("/validate")
    public ResponseEntity<?> validateToken(@RequestBody TokenRequest request){
        log.info(() -> "ValidatingToken");
        if(redisSessionService.getUserIdBySessionToken(request.getToken()) != null)
            return ResponseEntity.ok(true);
        else
            return ResponseEntity.ok(false);
    }
}
