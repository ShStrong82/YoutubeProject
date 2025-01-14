function getVideoId(url) {
  // Regular expression to match YouTube video IDs
  const regex =
    /(?:https?:\/\/(?:www\.)?youtube\.com\/(?:watch\?v=|embed\/|v\/)|https?:\/\/youtu\.be\/|youtube\.com\/shorts\/)([a-zA-Z0-9_-]{11})/;

  const match = url.match(regex);
  return match ? match[1] : null; // Return the videoId if found, otherwise null
}

// Event listener for the "Go" button
//document.getElementById("inputUrlBtn").addEventListener("click", function () {
//  const videoUrl = document.getElementById("input_Url").value; // Get the input value

//  if (!videoUrl) {
//    alert("لطفا لینک را وارد کنید");
//    return;
//  }

//  const videoId = getVideoId(videoUrl); // Extract the videoId

//  if (!videoId) {
//    alert("لینک اشتباه است");
//    return;
//  }

//  if (videoId) {
//    // Set the iframe source to embed the video
//    document.getElementById(
//      "video_iframe"
//    ).src = `https://www.youtube.com/embed/${videoId}`;
//    document.getElementById("videoDeatailsCard").style.visibility = "visible";
//    document.getElementById("transcriptText_Wrapper").style.visibility =
//      "visible";
//    document.getElementById(
//      "translated_TranscriptText_Wrapper"
//    ).style.visibility = "visible";
//  } else {
//    alert("لینک اشتباه است. لطفا مجددا تلاش کنید");
//  }

//  // Redirect to the Index action with the videoId as a query parameter
//  //window.location.href = `/Video?videoId=${videoId}`;
//});