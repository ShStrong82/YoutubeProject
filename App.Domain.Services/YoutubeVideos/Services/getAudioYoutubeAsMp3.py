# if want to run this script place ffmpeg.exe on folder


import yt_dlp

# does not work without proxy or vpn

#playlist_url = 'https://www.youtube.com/playlist?list=YOUR_PLAYLIST_ID'  

save_path = 'D:/Final_Project_Daneshgah/Youtube_Project/YoutubeProject/App.Domain.Services/YoutubeVideos/Services/AudioFiles' 

def download_audio_as_mp3(video_url, save_path=save_path):
    ydl_opts = {
        'outtmpl': save_path + '/%(title)s.%(ext)s',  # Save path and file name
        'format': 'bestaudio/best',
        'postprocessors': [{  # Post-process to convert to MP3
            'key': 'FFmpegExtractAudio',
            'preferredcodec': 'mp3',  # Convert to mp3
            'preferredquality': '192',  # '0' means best quality, auto-determined by source
        }],
    }
    with yt_dlp.YoutubeDL(ydl_opts) as ydl:
        ydl.download([video_url])
        


# video_url = 'https://www.youtube.com/watch?v=5bLPlL0fHOQ&pp=ygULaG9vayBwaXNocm8%3D'
# download_audio_as_mp3(video_url, save_path)



# # Serializing json
# json_object = json.dumps(response.json(), indent=4)
 
# # Writing to sample.json
# path = 'D:/Final_Project_Daneshgah/Backend/TranscriptServiceByPy/files/output.json'

# with open(path, "w") as outfile:
#     outfile.write(json_object)



# # getting url and normalize to videoId
# input_url = input("Enter url: ")

# video_id = input_url.replace('https://www.youtube.com/watch?v=', '')


