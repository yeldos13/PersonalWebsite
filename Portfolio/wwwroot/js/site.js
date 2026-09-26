(() => {
    const items = document.querySelectorAll(".reveal");
    if (!("IntersectionObserver" in window)) {
        items.forEach(el => el.classList.add("visible"));
    } else {
        const io = new IntersectionObserver(entries => {
            entries.forEach(e => {
                if (e.isIntersecting) {
                    e.target.classList.add("visible");
                    io.unobserve(e.target);
                }
            });
        }, { threshold: 0.12 });
        items.forEach(el => io.observe(el));
    }

    const links = [...document.querySelectorAll(".nav-links a")];
    const sections = links
        .map(a => document.querySelector(a.getAttribute("href")))
        .filter(Boolean);

    const onScroll = () => {
        const y = window.scrollY + 120;
        let current = sections[0];
        for (const s of sections) if (s.offsetTop <= y) current = s;
        links.forEach(a => a.classList.toggle("active", a.getAttribute("href") === "#" + current.id));
    };
    window.addEventListener("scroll", onScroll, { passive: true });
    onScroll();
})();
