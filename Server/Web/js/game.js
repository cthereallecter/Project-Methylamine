// Simple immediate initialization
function initializeGame() {
    console.log('Starting game initialization');
    
    const loading = document.getElementById('loading');
    const gameArea = document.getElementById('gameArea');
    const fallback = document.getElementById('flashFallback');
    
    // Hide loading immediately
    if (loading) {
        loading.style.display = 'none';
        console.log('Loading hidden');
    }
    
    // Clear game area
    if (gameArea) {
        gameArea.innerHTML = '';
        
        // Create embed element immediately
        const embed = document.createElement('embed');
        embed.src = 'Methylamine-Client.swf';
        embed.width = '800';
        embed.height = '600';
        embed.type = 'application/x-shockwave-flash';
        embed.style.display = 'block';
        embed.style.margin = '0 auto';
        embed.style.backgroundColor = '#222';
        
        gameArea.appendChild(embed);
        console.log('Embed element created and added');
        
        // Show fallback after 10 seconds if nothing happens
        setTimeout(() => {
            if (embed.offsetHeight <= 0 && fallback) {
                console.log('Embed failed, showing fallback');
                fallback.style.display = 'block';
            }
        }, 10000);
    }
}

// Run immediately when page loads
window.addEventListener('load', initializeGame);

// Also run immediately when script loads (backup)
if (document.readyState === 'complete') {
    initializeGame();
}

// Clean, simple game.js - no complex logic
console.log('Game script loaded successfully');

// Optional: Add click handler to enable audio (fixes Ruffle audio warnings)
document.addEventListener('click', function() {
    console.log('User interaction detected - audio should work now');
}, { once: true });