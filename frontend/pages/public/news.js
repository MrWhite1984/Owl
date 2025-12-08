(function () {
    'use strict';

    let nextDateTime = null;
    let isLoading = false;
    let hasMore = true;

    const newsContainer = document.getElementById('news-container');
    const loadingEl = document.getElementById('loading');
    const noMoreEl = document.getElementById('no-more');

    if (!newsContainer) return;

    function escapeHtml(text) {
        const div = document.createElement('div');
        div.textContent = text;
        return div.innerHTML;
    }

    async function loadNews() {
        if (isLoading || !hasMore) return;

        isLoading = true;
        if (loadingEl) loadingEl.style.display = 'block';

        const url = new URL('/api/News/get-part-news', window.location.origin);
        if (nextDateTime) {
            url.searchParams.append('beforeDateTime', nextDateTime.toISOString());
        }
        url.searchParams.append('partSize', '10');

        try {
            const response = await fetch(url, {
                method: 'GET',
                headers: { 'Accept': 'application/json' }
            });

            if (!response.ok) {
                throw new Error(`HTTP ${response.status}`);
            }

            const data = await response.json();

            if (!data?.news || !Array.isArray(data.news)) {
                hasMore = false;
                return;
            }

            data.news.forEach(newsItem => {
                const author = newsItem.author || {};
                const isVerified = author.isVerified === true;

                const verifiedBadge = isVerified
                    ? '<div class="flex items-center gap-1">' +
                      '<svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 text-green-500" viewBox="0 0 20 20" fill="currentColor">' +
                      '<path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clip-rule="evenodd"/>' +
                      '</svg>' +
                      '<span class="text-xs text-green-600">Подтверждён</span>' +
                      '</div>'
                    : '';

                const surname = escapeHtml(author.surname || '');
                const name = escapeHtml(author.name || '');
                const patronymic = escapeHtml(author.patronymic || '');
                const jobTitle = escapeHtml(author.jobTitle || '');
                const title = escapeHtml(newsItem.title || '');
                const content = escapeHtml(newsItem.content || '');
                const date = newsItem.createdAt
                    ? new Date(newsItem.createdAt).toLocaleDateString('ru')
                    : '';

                const article = document.createElement('article');
                article.className = 'border border-base-300 rounded-lg p-5 hover:shadow-md transition-shadow';
                article.innerHTML =
                    '<h2 class="text-xl font-semibold mb-2">' + title + '</h2>' +
                    '<div class="text-sm text-gray-500 mb-3 flex items-center gap-4">' +
                        '<span>' + date + '</span>' +
                        '<div class="flex items-center gap-1">' +
                            '<span class="font-medium">' + surname + ' ' + name + ' ' + patronymic + '</span>' +
                            '<span>—</span>' +
                            '<span>' + jobTitle + '</span>' +
                        '</div>' +
                        verifiedBadge +
                    '</div>' +
                    '<p>' + content + '</p>';

                newsContainer.appendChild(article);
            });

            nextDateTime = data.nextDateTime ? new Date(data.nextDateTime) : null;
            hasMore = !!nextDateTime;

            if (!hasMore && noMoreEl) {
                noMoreEl.style.display = 'block';
            }
        } catch (err) {
            console.error('Ошибка загрузки новостей:', err);
        } finally {
            isLoading = false;
            if (loadingEl) loadingEl.style.display = 'none';
        }
    }

    function isNearBottom() {
        return window.innerHeight + window.scrollY >= document.body.offsetHeight - 500;
    }

    function init() {
        loadNews();
        window.addEventListener('scroll', () => {
            if (isNearBottom()) {
                loadNews();
            }
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();