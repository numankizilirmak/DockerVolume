# DockerVolume
Asp.net core docker volume
in powershell go to folser that Dockerfile in and create image with script below

docker create --name container_adı containerdakullanılacakimageadı

then create volume

docker volume create images 

then create container that run project on 3000 port

docker run -d -p 3000:4500 --name dockerVolumeExample -v images:/app/wwwroot/images your_image_id 
