<script>
  const xhr = new XMLHttpRequest();

  xhr.open(
    "GET",
    "https://i.ytimg.com/sb/596DNAXmLmA/storyboard3_L3/M19.jpg?sqp=-oaymwENSDfyq4qpAwVwAcABBqLzl_8DBgi62LK1Bg==&sigh=rs$AOn4CLAp0n12cZfpQwPPuXRRDfWSsjYzCg",
  );

  xhr.send();

  xhr.onload = e => {
    console.log(xhr.status);
  };
</script>
