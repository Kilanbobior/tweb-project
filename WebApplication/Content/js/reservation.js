
  const checkin = document.getElementById('checkin');
  const checkout = document.getElementById('checkout');
  const guestsSummary = document.getElementById('guestsSummary');
  const adultsInput = document.getElementById('adults');
  const childrenInput = document.getElementById('children');
  const calendarDropdown = document.getElementById('calendarDropdown');
  const inputGroup = document.getElementById('dateInput');
  const confirmBtn = document.querySelector('.confirm');

  let selectedDates = [];

  inputGroup.addEventListener('click', () => {
    calendarDropdown.style.display = 'flex';
  });

  flatpickr("#calendar", {
    mode: "range",
    inline: true,
    locale: "ru",
    dateFormat: "Y-m-d",
    onChange: function (dates) {
      selectedDates = dates;
      if (dates.length === 2) {
        checkin.value = dates[0].toLocaleDateString();
        checkout.value = dates[1].toLocaleDateString();
      }
    }
  });

  confirmBtn.addEventListener('click', () => {
    const adults = parseInt(adultsInput.value);
    const children = parseInt(childrenInput.value);

    let text = `${adults} взросл${adults === 1 ? 'ый' : 'ых'}`;
    if (children > 0) {
      text += `, ${children} дет${children === 1 ? 'ёныш' : 'ей'}`;
    }

    guestsSummary.value = text;
    calendarDropdown.style.display = 'none';
  });

  document.addEventListener('click', (e) => {
    if (!inputGroup.contains(e.target) && !calendarDropdown.contains(e.target)) {
      calendarDropdown.style.display = 'none';
    }
  });

