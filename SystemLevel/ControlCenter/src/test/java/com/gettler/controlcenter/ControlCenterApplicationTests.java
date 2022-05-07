package com.gettler.controlcenter;

import com.gettler.controlcenter.mapper.UserMapper;
import com.gettler.controlcenter.pojo.User;
import com.gettler.controlcenter.pojo.UserExample;
import com.gettler.controlcenter.util.MybatisUtils;
import com.gettler.controlcenter.vo.Result;
import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.List;

@SpringBootTest
class ControlCenterApplicationTests {

    @Test
    void contextLoads() {
    }

    @Test
    void testRegister() {
        UserMapper userMapper = MybatisUtils.getSqlseesion().getMapper(UserMapper.class);
        UserExample userExample = new UserExample();
//        String username = "Gettler";
//        String password = "Gettler";
//        userExample.createCriteria().andUsernameEqualTo(username);
//        List<User> user = userMapper.selectByExample(userExample);
//        if (user.isEmpty()) {
//            System.out.println(userMapper.insert(new User(username, password)));
//        }
//        System.out.println(userMapper.selectByExample(null));
//    }
//        UserExample userExample = new UserExample();
        userExample.createCriteria().andUsernameEqualTo("Gettler").andPasswordEqualTo("Gettler");
        List<User> users = userMapper.selectByExample(userExample);
        if (users.isEmpty()) System.out.println(Result.success("原密码错误"));
//        userMapper.updateByExample(new User("Gettler", "73748156"), userExample);
        else
            System.out.println(Result.success(userMapper.updateByExample(new User(users.get(0).getUserid(), "Gettler", "73748156"), userExample)));
    }
}
