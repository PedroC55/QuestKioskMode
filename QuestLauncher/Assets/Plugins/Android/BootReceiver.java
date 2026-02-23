package com.hubduction.questlauncher;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Handler;

public class BootReceiver extends BroadcastReceiver {
    @Override
    public void onReceive(Context context, Intent intent) {
        final Context appContext = context.getApplicationContext();
        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                Intent launch = new Intent();
                launch.setClassName("com.tdv.vrapp", "org.mozilla.vrbrowser.VRBrowserActivity");
                launch.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                launch.addFlags(Intent.FLAG_ACTIVITY_REORDER_TO_FRONT);
                appContext.startActivity(launch);
            }
        }, 10000);
    }
}