/******************************************************************************
 *
 * File:         render.cpp  (Formerly render.c)
 * Description:  Convert the various data type into line lists
 * Author:       Mark Seaman, OCR Technology
 *
 * (c) Copyright 1989, Hewlett-Packard Company.
 ** Licensed under the Apache License, Version 2.0 (the "License");
 ** you may not use this file except in compliance with the License.
 ** You may obtain a copy of the License at
 ** http://www.apache.org/licenses/LICENSE-2.0
 ** Unless required by applicable law or agreed to in writing, software
 ** distributed under the License is distributed on an "AS IS" BASIS,
 ** WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 ** See the License for the specific language governing permissions and
 ** limitations under the License.
 *
 *****************************************************************************/

// Include automatically generated configuration file if running autoconf.
#ifdef HAVE_CONFIG_H
#  include "config_auto.h"
#endif

#include "render.h"

#include "blobs.h"

#include <cmath>

namespace tesseract {

/*----------------------------------------------------------------------
              V a r i a b l e s
----------------------------------------------------------------------*/
ScrollView *blob_window = nullptr;

ScrollView::Color color_list[] = {ScrollView::RED,  ScrollView::CYAN,  ScrollView::YELLOW,
                                  ScrollView::BLUE, ScrollView::GREEN, ScrollView::WHITE};

BOOL_VAR(wordrec_display_all_blobs, 0, "Display Blobs");

BOOL_VAR(wordrec_blob_pause, 0, "Blob pause");

/*----------------------------------------------------------------------
              F u n c t i o n s
----------------------------------------------------------------------*/
#ifndef GRAPHICS_DISABLED
/**********************************************************************
 * display_blob
 *
 * Macro to display blob in a window.
 **********************************************************************/
void display_blob(TBLOB *blob, ScrollView::Color color) {
  /* Size of drawable */
  if (blob_window == nullptr) {
    blob_window = new ScrollView("Blobs", 520, 10, 500, 256, 2000, 256, true);
  } else {
    blob_window->Clear();
  }

  render_blob(blob_window, blob, color);
}

/**********************************************************************
 * render_blob
 *
 * Create a list of line segments that represent the expanded outline
 * that was supplied as input.
 **********************************************************************/
void render_blob(ScrollView *window, TBLOB *blob, ScrollView::Color color) {
  /* No outline */
  if (!blob) {
    return;
  }

  render_outline(window, blob->outlines, color);
}

/**********************************************************************
 * render_edgepts
 *
 * Create a list of line segments that represent the expanded outline
 * that was supplied as input.
 **********************************************************************/
void render_edgepts(ScrollView *window, EDGEPT *edgept, ScrollView::Color color) {
  if (!edgept) {
    return;
  }

  float x = edgept->pos.x;
  float y = edgept->pos.y;
  EDGEPT *this_edge = edgept;

  window->Pen(color);
  window->SetCursor(x, y);
  do {
    this_edge = this_edge->next;
    x = this_edge->pos.x;
    y = this_edge->pos.y;
    window->DrawTo(x, y);
  } while (edgept != this_edge);
}

/**********************************************************************
 * render_outline
 *
 * Create a list of line segments that represent the expanded outline
 * that was supplied as input.
 **********************************************************************/
void render_outline(ScrollView *window, TESSLINE *outline, ScrollView::Color color) {
  /* No outline */
  if (!outline) {
    return;
  }
  /* Draw Compact outline */
  if (outline->loop) {
    render_edgepts(window, outline->loop, color);
  }
  /* Add on next outlines */
  render_outline(window, outline->next, color);
}

#endif // !GRAPHICS_DISABLED

} // namespace tesseract
