const track = document.querySelector('.testimonial-track');
  const cardWidth = 320; // 300 + 20px gap
  let index = 0;

  document.getElementById('nextBtn').addEventListener('click', () => {
    if (index < track.children.length - 1) {
      index++;
      track.style.transform = `translateX(-${cardWidth * index}px)`;
    }
  });

  document.getElementById('prevBtn').addEventListener('click', () => {
    if (index > 0) {
      index--;
      track.style.transform = `translateX(-${cardWidth * index}px)`;
    }
  });