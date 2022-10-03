using Enum;
using Managers;
using UnityEngine;

namespace Commands
{
    public class DropZonePlacerCommand
    {

        #region Eski Hali
        
        private DropZoneManager _dropZoneManager;
        private int _currentHeight;
        private int _currentRowIndex;
        private int _currentColumnIndex;
        #endregion

        public Vector3 DropZonePlacer(DropZoneManager dropZoneManager,Transform stackHolder, int maxRowSize, int maxColumnSize, int maxHeigthSize,float heigthOffset,float columnOffset,float rowOffset)
        {
            Vector3 _nextPlacePos;
            _dropZoneManager = dropZoneManager;
            _nextPlacePos =-new Vector3(
                   -((stackHolder.localScale.x / maxRowSize) * _currentRowIndex+rowOffset)
                   ,-_currentHeight*heigthOffset
                   ,-((stackHolder.localScale.z / maxColumnSize)*_currentColumnIndex)+columnOffset);
            _currentRowIndex++;
            if (_currentRowIndex==maxRowSize)
            {
                _currentRowIndex = 0;
                _currentColumnIndex++;
            }
            if (_currentColumnIndex == maxColumnSize)
            {
                _currentColumnIndex = 0;
                _currentHeight++;
                dropZoneManager.IsStackFull=(_currentHeight == maxHeigthSize)?true:false;
            }
            return _nextPlacePos;

        }

        public void ResetDropZone()
        {
            _currentRowIndex = 0;
            _currentColumnIndex = 0;
            _currentHeight = 0;
        }
    }
}