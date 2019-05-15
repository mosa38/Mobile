package com.player.lab4;

import android.media.MediaPlayer;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.SeekBar;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    Button playBtn;
    SeekBar positionBar;
    SeekBar volumeBar;
    TextView elapsedTimeLabel;
    TextView remainingTimeLabel;
    TextView tvSongText;
    MediaPlayer mp;
    int totalTime;
    int index =0;
    String[] data = {"West Coast", "Break up with your girlfriend", "Capades", "Him and I"};
    int[] rawData = {R.raw.west_coast, R.raw.break_up_with_your_girlfriend, R.raw.capades, R.raw.him_and_i};
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        playBtn = (Button) findViewById(R.id.playBtn);
        elapsedTimeLabel = (TextView) findViewById(R.id.elapsedTimeLabel);
        remainingTimeLabel = (TextView) findViewById(R.id.remainingTimeLabel);
        tvSongText = findViewById(R.id.tv_song_name);

        prepareMediaPlayer();

        positionBar = (SeekBar) findViewById(R.id.positionBar);
        positionBar.setMax(totalTime);
        positionBar.setOnSeekBarChangeListener(
                new SeekBar.OnSeekBarChangeListener() {
                    @Override
                    public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                        if (fromUser) {
                            mp.seekTo(progress);
                            positionBar.setProgress(progress);
                        }
                    }

                    @Override
                    public void onStartTrackingTouch(SeekBar seekBar) {

                    }

                    @Override
                    public void onStopTrackingTouch(SeekBar seekBar) {

                    }
                }
        );
        volumeBar = (SeekBar) findViewById(R.id.volumeBar);
        volumeBar.setOnSeekBarChangeListener(
                new SeekBar.OnSeekBarChangeListener() {
                    @Override
                    public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                        float volumeNum = progress / 100f;
                        mp.setVolume(volumeNum, volumeNum);
                    }

                    @Override
                    public void onStartTrackingTouch(SeekBar seekBar) {

                    }

                    @Override
                    public void onStopTrackingTouch(SeekBar seekBar) {

                    }
                }
        );

        new Thread(new Runnable() {
            @Override
            public void run() {
                while (mp != null) {
                    try {
                        Thread.sleep(1000);
                        final int currentPosition = mp.getCurrentPosition();
                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                positionBar.setProgress(currentPosition);

                                String elapsedTime = createTimeLabel(currentPosition);
                                elapsedTimeLabel.setText(elapsedTime);

                                String remainingTime = createTimeLabel(totalTime-currentPosition);
                                remainingTimeLabel.setText("- " + remainingTime);
                            }
                        });
                    } catch (InterruptedException e) {}
                }
            }
        }).start();

    }

    public String createTimeLabel(int time) {
        String timeLabel = "";
        int min = time / 1000 / 60;
        int sec = time / 1000 % 60;

        timeLabel = min + ":";
        if (sec < 10) timeLabel += "0";
        timeLabel += sec;

        return timeLabel;
    }

    public void playBtnClick(View view) {


        changePlayPause();
    }

    private void changePlayPause() {
        if (!mp.isPlaying()) {
            // Stopping
            mp.start();
            playBtn.setBackgroundResource(R.drawable.stop);

        } else {
            // Playing
            mp.pause();
            playBtn.setBackgroundResource(R.drawable.play);
        }
    }

    public void nextButtonClick(View view){
        mp.pause();
        mp.release();
        if(index!=rawData.length-1)
            index++;
        prepareMediaPlayer();
        changePlayPause();

    }

    public void prevButtonClick(View view){
        mp.pause();
        mp.release();
        if(index!=0)
            index--;
        prepareMediaPlayer();
        changePlayPause();
    }

    private void prepareMediaPlayer(){
        mp=new MediaPlayer();
        mp = MediaPlayer.create(this, rawData[index]);
        mp.setLooping(true);
        mp.seekTo(0);
        mp.setVolume(0.5f, 0.5f);
        totalTime = mp.getDuration();
        tvSongText.setText(data[index]);
    }
}
