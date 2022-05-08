package com.gettler.controlcenter.util;

import com.gettler.controlcenter.mapper.PortMapper;
import com.gettler.controlcenter.pojo.Port;
import com.sun.jna.Platform;
import org.springframework.util.StringUtils;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.lang.reflect.Field;
import java.nio.charset.StandardCharsets;
import java.util.List;

/**
 * @author Black
 */
public class ShellPower {
    public static String getProcessId(Process process) {
        long pid = -1;
        Field field = null;
        if (Platform.isWindows()) {
            try {
                field = process.getClass().getDeclaredField("handle");
                field.setAccessible(true);
                pid = Kernel32.INSTANCE.GetProcessId((Long) field.get(process));
            } catch (Exception ex) {
                ex.printStackTrace();
            }
        } else if (Platform.isLinux() || Platform.isAIX()) {
            try {
                Class<?> clazz = Class.forName("java.lang.UNIXProcess");
                field = clazz.getDeclaredField("pid");
                field.setAccessible(true);
                pid = (Integer) field.get(process);
            } catch (Throwable e) {
                e.printStackTrace();
            }
        }
        return String.valueOf(pid);
    }

    /**
     * 关闭Linux进程
     *
     * @param pid 进程的PID
     */
    public static boolean killProcessByPid(String pid) {
        if (StringUtils.isEmpty(pid) || "-1".equals(pid)) {
            throw new RuntimeException("Pid ==" + pid);
        }
        Process process = null;
        BufferedReader reader = null;
        String command = "";
        boolean result = false;
        if (Platform.isWindows()) {
            command = "cmd.exe /c taskkill /PID " + pid + " /F /T ";
        } else if (Platform.isLinux() || Platform.isAIX()) {
            command = "kill -9 " + pid;
        }
        try {
            //杀掉进程
            process = Runtime.getRuntime().exec(command);
            reader = new BufferedReader(new InputStreamReader(process.getInputStream(), "utf-8"));
//            String line = null;
//            while((line = reader.readLine())!=null){
//                log.info("kill PID return info -----> "+line);
//            }
            result = true;
        } catch (Exception e) {
//            log.info("杀进程出错：", e);
            result = false;
        } finally {
            if (process != null) {
                process.destroy();
            }
            if (reader != null) {
                try {
                    reader.close();
                } catch (IOException e) {

                }
            }
        }
        return result;
    }

    public static int addPort(Integer num) throws IOException {
        Process proc = Runtime.getRuntime().exec("echo " + num.toString() + " | dotnet /home/C#/out/ServerConsole.dll > /home/C#/logs/" + num.toString() + ".txt");
        return Integer.parseInt(getProcessId(proc));
    }

    public static void deletePort(Integer userid) {
        PortMapper portMapper = MybatisUtils.getSqlseesion().getMapper(PortMapper.class);
        Port port = portMapper.selectByPrimaryKey(userid);
        killProcessByPid(port.getPid().toString());
    }

}
