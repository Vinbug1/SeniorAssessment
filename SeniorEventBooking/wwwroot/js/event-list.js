document.addEventListener("DOMContentLoaded", () => {
  const eventItems = document.querySelectorAll(".event-item");
  const detailContainer = document.getElementById("eventDetail");

  if (!eventItems.length || !detailContainer) return;

  eventItems.forEach(item => {
    item.addEventListener("click", async (e) => {
      e.preventDefault();

      const url = item.getAttribute("data-url");
      if (!url) return;

      // Show loading state
      detailContainer.innerHTML = "<p>⏳ Loading event details...</p>";

      try {
        const response = await fetch(url, { headers: { "X-Requested-With": "XMLHttpRequest" } });

        if (!response.ok) throw new Error(`Failed to load event details (HTTP ${response.status})`);

        const html = await response.text();

        // Parse the HTML and extract relevant content
        const parser = new DOMParser();
        const doc = parser.parseFromString(html, "text/html");

        // Try finding a specific section (like the event detail area)
        const eventContent = doc.querySelector(".event-detail, article, main");

        if (eventContent) {
          detailContainer.innerHTML = eventContent.innerHTML;
          detailContainer.scrollIntoView({ behavior: "smooth", block: "start" });
        } else {
          detailContainer.innerHTML = "<p>⚠️ Event details not found on this page.</p>";
        }

      } catch (err) {
        console.error("Error loading event detail:", err);
        detailContainer.innerHTML = `<p style="color:red;">❌ ${err.message}</p>`;
      }
    });
  });
});

