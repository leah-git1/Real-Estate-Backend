# =========================================
# Download 120 Images - Verified Version (Picsum)
# Structure: 30 Products x 4 Images each
# Save in wwwroot/images
# =========================================

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

$basePath = Join-Path $PSScriptRoot "wwwroot\images"

if (!(Test-Path $basePath)) {
    New-Item -ItemType Directory -Path $basePath | Out-Null
}

$counter = 1
# רשימת ID של תמונות ב-Picsum שהן בוודאות של בתים/נוף/אדריכלות
$architectureIds = @(10, 43, 57, 58, 59, 75, 84, 101, 122, 164, 177, 188, 190, 211, 221, 230, 234, 238, 249, 259)

for ($product = 1; $product -le 30; $product++) {
    Write-Host "Processing Product $product..." -ForegroundColor Cyan
    for ($imgNum = 1; $imgNum -le 4; $imgNum++) {

        $fileName = "property_${product}_${imgNum}.jpg"
        $target = Join-Path $basePath $fileName
        
        # אם נגמרה הרשימה המוגדרת, הוא פשוט ייקח רנדומלי לפי ה-counter
        $id = if ($counter -lt $architectureIds.Count) { $architectureIds[$counter] } else { $counter + 100 }
        $url = "https://picsum.photos/id/$id/800/600"

        if (!(Test-Path $target)) {
            try {
                Invoke-WebRequest -Uri $url -OutFile $target -UseBasicParsing
                Write-Host "Success: $fileName (ID: $id)" -ForegroundColor Green
            } catch {
                # אם ה-ID ספציפי נכשל, ננסה רנדומלי רגיל כגיבוי
                $backupUrl = "https://picsum.photos/800/600?random=$counter"
                Invoke-WebRequest -Uri $backupUrl -OutFile $target -UseBasicParsing
                Write-Host "Used Backup for $fileName" -ForegroundColor Yellow
            }
            # השהייה קצרה כדי לא להעמיס
            Start-Sleep -Milliseconds 300
        }
        $counter++
    }
}

Write-Host "Finished! 120 images are ready in $basePath" -ForegroundColor Magenta