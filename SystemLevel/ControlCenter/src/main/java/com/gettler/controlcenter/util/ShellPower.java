package com.gettler.controlcenter.util;

import com.gettler.controlcenter.mapper.PortMapper;
import com.gettler.controlcenter.pojo.Port;
import com.sun.jna.Platform;
import lombok.SneakyThrows;

import java.io.BufferedReader;
import java.io.IOException;
import java.lang.reflect.Field;
import java.util.ArrayList;
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
    public static void killProcessByPid(String pid) throws IOException {
//        if (StringUtils.isEmpty(pid) || "-1".equals(pid)) {
//            throw new RuntimeException("Pid ==" + pid);
//        }
//        Process process = null;
//        BufferedReader reader = null;
//        String command = "";
//        boolean result = false;
//        if (Platform.isWindows()) {
//            command = "cmd.exe /c taskkill /PID " + pid + " /F /T ";
//        } else if (Platform.isLinux() || Platform.isAIX()) {
        String command = "kill -9 " + pid;
//        }
        System.out.println(pid);
//        try {
//            //杀掉进程
//            process = Runtime.getRuntime().exec(command);
//            reader = new BufferedReader(new InputStreamReader(process.getInputStream(), "utf-8"));
////            String line = null;
////            while((line = reader.readLine())!=null){
////                log.info("kill PID return info -----> "+line);
////            }
//            result = true;
//        } catch (Exception e) {
////            log.info("杀进程出错：", e);
////            result = false;
//        } finally {
//            if (process != null) {
//                process.destroy();
//            }
//            if (reader != null) {
//                try {
//                    reader.close();
//                } catch (IOException e) {
//
//                }
//            }
//        }
        List<String> cmds = new ArrayList<String>();
        cmds.add("sh");
        cmds.add("-c");
        cmds.add(command);
        ProcessBuilder pb = new ProcessBuilder(cmds);
        Process p = pb.start();
    }

    public static int addPort(Integer num) throws IOException {
        List<String> cmds = new ArrayList<String>();
        cmds.add("sh");
        cmds.add("-c");
        cmds.add("echo " + num.toString() + " | dotnet /home/C#/out/ServerConsole.dll > /home/C#/logs/" + num.toString() + ".txt");
        ProcessBuilder pb = new ProcessBuilder(cmds);
        Process p = pb.start();
        return Integer.parseInt(getProcessId(p));
    }

    @SneakyThrows
    public static void deletePort(Integer pid) {
        killProcessByPid(pid.toString());
    }

}
