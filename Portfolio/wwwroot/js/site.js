(() => {
    const root = document.documentElement;
    const toggle = document.getElementById("theme-toggle");
    const currentTheme = () => root.getAttribute("data-theme") || "light";

    toggle?.addEventListener("click", () => {
        const next = currentTheme() === "light" ? "dark" : "light";
        root.setAttribute("data-theme", next);
        try { localStorage.setItem("theme", next); } catch { }
    });

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

    const nav = document.getElementById("nav");

    const onScroll = () => {
        nav?.classList.toggle("scrolled", window.scrollY > 4);
        const y = window.scrollY + 120;
        let current = null;
        for (const s of sections) if (s.offsetTop <= y) current = s;
        links.forEach(a => a.classList.toggle("active", current !== null && a.getAttribute("href") === "#" + current.id));
    };
    window.addEventListener("scroll", onScroll, { passive: true });
    onScroll();

    const clickTarget = href =>
        href.startsWith("tel:") ? "phone" :
        href.startsWith("mailto:") ? "email" :
        href.includes("t.me/") ? "telegram" :
        href.includes("linkedin.com") ? "linkedin" :
        /github\.com\/yeldos13\/./.test(href) ? "project" :
        href.includes("github.com") ? "github" : null;
    document.addEventListener("click", e => {
        const a = e.target.closest("a[href]");
        const t = a && clickTarget(a.getAttribute("href"));
        if (t) navigator.sendBeacon?.("/t?e=" + t);
    });
})();
