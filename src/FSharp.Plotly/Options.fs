﻿namespace FSharp.Plotly

open System
open ApplyHelper

type TraceOptions<'T when 'T :> Trace>  = 'T -> 'T
type FontOptions   = Font -> Font
type LineOptions   = Line -> Line
type MarkerOptions = Marker -> Marker
type ErrorOptions  = Error -> Error
type LayoutOptions = Layout -> Layout
type MarginOptions = Margin -> Margin
type AxisOptions   = LinearAxis -> LinearAxis
type RadialAxisOptions = Radialaxis -> Radialaxis
type AngularAxisOptions   = Angularaxis -> Angularaxis
type SceneOptions  = Scene -> Scene
type LegendOptions = Legend -> Legend
type ShapeOptions = Shape -> Shape

/// Functions provide the styling of the Chart objects
type Options() =
    
    // Applies the styles to generic Trace()
    static member Trace
        (    
            ?Name: string,
            ?Visible: StyleOption.Visible,
            ?Showlegend: bool,
            ?Legendgroup:string,
            ?Opacity: float,
            ?Uid: string,
            ?Hoverinfo: string,
            ?Stream: Stream

        ) =
            (fun (trace:('T :> Trace)) ->  
                Name        |> Option.iter trace.set_name     
                Visible     |> Option.iter (StyleOption.Visible.toString >> trace.set_visible)     
                Showlegend  |> Option.iter trace.set_showlegend
                Legendgroup |> Option.iter trace.set_legendgroup  
                Opacity     |> Option.iter trace.set_opacity 
                Uid         |> Option.iter trace.set_uid 
                Hoverinfo   |> Option.iter trace.set_hoverinfo
                // Update
                //Stream: Stream                    
                
                // out ->
                trace
            )  

    // Applies the styles to Font()
    static member Font
        (    
            ?Family: StyleOption.FontFamily,
            ?Size,
            ?Color,
            ?Familysrc,
            ?Sizesrc,
            ?Colorsrc
        ) =
            (fun (font:('T :> Font)) -> 
                Family       |> Option.iter (StyleOption.FontFamily.toString >> font.set_family) 
                Size         |> Option.iter font.set_size     
                Color        |> Option.iter font.set_color    
                Familysrc    |> Option.iter font.set_familysrc
                Sizesrc      |> Option.iter font.set_sizesrc  
                Colorsrc     |> Option.iter font.set_colorsrc 
                    
                // out ->
                font
            )   

    // Applies the styles to Line()
    static member Line
        (
            ?Width,
            ?Color,
            ?Shape:StyleOption.Shape,
            ?Dash,
            ?Smoothing,
            ?ColorScale:StyleOption.ColorScale
        ) =
            (fun (line:('T :> Line)) -> 
                Color      |> Option.iter line.set_color
                Width      |> Option.iter line.set_width
                Shape      |> Option.iter (StyleOption.Shape.convert >> line.set_shape)
                Smoothing  |> Option.iter line.set_smoothing
                Dash       |> Option.iter (StyleOption.DrawingStyle.toString >> line.set_dash)
                ColorScale |> Option.iter (StyleOption.ColorScale.convert >> line.set_colorscale)
                // out -> 
                line
            )

    // Applies the styles to Marker()
    static member Marker
        (   
            ?Size:int,
            ?Color,
            ?Symbol:StyleOption.Symbol,
            ?Opacity:float,
            ?MultiSizes:seq<#IConvertible>
        ) =
            (fun (marker:('T :> Marker)) -> 
                Size       |> Option.iter marker.set_size 
                Color      |> Option.iter marker.set_color
                Symbol     |> Option.iter marker.set_symbol
                Opacity    |> Option.iter marker.set_opacity
                MultiSizes |> Option.iter marker.set_size
            
                marker
            )

    // Applies the styles to Scatter()
    static member Scatter
        (   
            //plotType: string, 
            ?TraceOptions:TraceOptions<_>,
            // data
            ?X      : seq<#IConvertible>,
            ?Y      : seq<#IConvertible>,
            ?Text   : seq<#IConvertible>,
            ?Textposition: StyleOption.TextPosition,
            ?Textfont: FontOptions,

            ?Hoverinfo: string,
            
            ?Mode: StyleOption.Mode, 
            ?Line: LineOptions,                         
            ?Marker: MarkerOptions,
            
            ?Fill: StyleOption.Fill,
            ?Fillcolor: string,
                        

            ?Uid: string, ?Stream: Stream, ?Connectgaps: bool, ?R: _, ?T: _,
            ?Error_y: ErrorOptions,
            ?Error_x: ErrorOptions
        ) =
            (fun (scatter:('T :> Scatter)) -> 
                //scatter.set_type plotType                     
                Uid          |> Option.iter scatter.set_uid
                Hoverinfo    |> Option.iter scatter.set_hoverinfo
                Stream       |> Option.iter scatter.set_stream
                X            |> Option.iter scatter.set_x
                Y            |> Option.iter scatter.set_y
                Text         |> Option.iter scatter.set_text
                Textposition |> Option.iter (StyleOption.TextPosition.toString >> scatter.set_textposition)
                Mode         |> Option.iter (StyleOption.Mode.toString >> scatter.set_mode)
                Connectgaps  |> Option.iter scatter.set_connectgaps
                Fill         |> Option.iter (StyleOption.Fill.toString >> scatter.set_fill)
                Fillcolor    |> Option.iter scatter.set_fillcolor                    
                R            |> Option.iter scatter.set_r
                T            |> Option.iter scatter.set_t
                // Update
                Line         |> Option.iter (updatePropertyValueAndIgnore scatter <@ scatter.line     @>)
                Marker       |> Option.iter (updatePropertyValueAndIgnore scatter <@ scatter.marker   @>)
                Textfont     |> Option.iter (updatePropertyValueAndIgnore scatter <@ scatter.textfont @>)
                Error_x      |> Option.iter (updatePropertyValueAndIgnore scatter <@ scatter.error_x  @>)
                Error_y      |> Option.iter (updatePropertyValueAndIgnore scatter <@ scatter.error_y  @>)
                    
                // out ->
                scatter |> (optApply TraceOptions) 
            ) 

    // Applies the styles to Bar()
    static member Bar
        (   
            //plotType: string, 
            ?TraceOptions:TraceOptions<_>, 
            // data
            ?X      : seq<#IConvertible>,
            ?Y      : seq<#IConvertible>,
            ?Text   : seq<#IConvertible>,

            ?Hoverinfo: string,                                   
            ?Marker: MarkerOptions,            
            ?Opacity: float,            

            ?Uid: string, ?Stream: Stream, ?R: _, ?T: _,
            ?Error_y: ErrorOptions,
            ?Error_x: ErrorOptions,
            // 
            ?Orientation
        ) =
            (fun (bar:('T :> Bar)) -> 
                //bar.set_type plotType                     
                Opacity      |> Option.iter bar.set_opacity
                Uid          |> Option.iter bar.set_uid
                Hoverinfo    |> Option.iter bar.set_hoverinfo
                Stream       |> Option.iter bar.set_stream
                X            |> Option.iter bar.set_x
                Y            |> Option.iter bar.set_y
                Text         |> Option.iter bar.set_text
                //form scattern --> textposition |> Option.iter (StyleOption.TextPosition.toString >> bar.set_textposition)
                //form scattern --> mode         |> Option.iter (StyleOption.Mode.toString >> bar.set_mode)
                //form scattern --> connectgaps  |> Option.iter bar.set_connectgaps
                //form scattern --> fill         |> Option.iter (StyleOption.Fill.toString >> bar.set_fill)
                //form scattern --> fillcolor    |> Option.iter bar.set_fillcolor                    
                R            |> Option.iter bar.set_r
                T            |> Option.iter bar.set_t
                Orientation  |> Option.iter (StyleOption.Orientation.convert >> bar.set_orientation)
                    
                // Update
                //form scattern --> line         |> Option.iter (updatePropertyValueAndIgnore bar <@ bar.line     @>)
                Marker       |> Option.iter (updatePropertyValueAndIgnore bar <@ bar.marker   @>)
                //form scattern --> textfont     |> Option.iter (updatePropertyValueAndIgnore bar <@ bar.textfont @>)
                Error_x      |> Option.iter (updatePropertyValueAndIgnore bar <@ bar.error_x  @>)
                Error_y      |> Option.iter (updatePropertyValueAndIgnore bar <@ bar.error_y  @>)
                    
                // out ->
                bar |> (optApply TraceOptions) 

            ) 


    // Applies the styles to Error()
    static member Error
        (   
            errorType,
            ?Symmetric,
            ?Array,
            ?Arrayminus,
            ?Value,
            ?Valueminus,
            ?Traceref,
            ?Tracerefminus,
            ?Copy_ystyle,
            ?Copy_zstyle,
            ?Color,
            ?Thickness,
            ?Width,
            ?Arraysrc,
            ?Arrayminussrc
        ) =
            (fun (error:('T :> Error)) -> 
                error.set_type errorType
                Symmetric     |> Option.iter error.set_symmetric
                Array         |> Option.iter error.set_array
                Arrayminus    |> Option.iter error.set_arrayminus
                Value         |> Option.iter error.set_value
                Valueminus    |> Option.iter error.set_valueminus
                Traceref      |> Option.iter error.set_traceref
                Tracerefminus |> Option.iter error.set_tracerefminus
                Copy_ystyle   |> Option.iter error.set_copy_ystyle
                Copy_zstyle   |> Option.iter error.set_copy_zstyle
                Color         |> Option.iter error.set_color
                Thickness     |> Option.iter error.set_thickness
                Width         |> Option.iter error.set_width
                Arraysrc      |> Option.iter error.set_arraysrc
                Arrayminussrc |> Option.iter error.set_arrayminussrc

                // out ->
                error
            )


    // Applies the styles to Layout()
    // TODO: obj to fun
    static member Layout
        (   
            ?Title,
            ?Titlefont:FontOptions,
            ?Font:FontOptions,
            ?Showlegend:bool,
            ?Autosize:bool,
            ?Width,
            ?Height,
            ?xAxis:AxisOptions,?xAxis2:AxisOptions,
            ?yAxis:AxisOptions,?yAxis2:AxisOptions,
            ?Legend:LegendOptions,
            // TODO: annotations
            ?Annotations,
            ?Margin:MarginOptions,
            
            ?Paper_bgcolor,
            ?Plot_bgcolor,
            ?Hovermode:StyleOption.Hovermode,
            ?Dragmode:StyleOption.Dragmode,
            
            ?Separators,
            ?Barmode:StyleOption.Barmode,
            ?Bargap, // Some bar.. /box... is missing
            ?Radialaxis:RadialAxisOptions,
            ?Angularaxis:AngularAxisOptions,
            ?Scene:SceneOptions,
            ?Direction:StyleOption.Direction,
            ?Orientation,
            ?Shapes:ShapeOptions,
            
            ?Hidesources,?Smith,?Geo

        ) =
            (fun (layout:('T :> Layout)) -> 
                Title |> Option.iter layout.set_title
                Autosize |> Option.iter layout.set_autosize
                Width |> Option.iter layout.set_width
                Height |> Option.iter layout.set_height
            
                Paper_bgcolor |> Option.iter layout.set_paper_bgcolor
                Plot_bgcolor |> Option.iter layout.set_plot_bgcolor
                Separators |> Option.iter layout.set_separators
                Hidesources |> Option.iter layout.set_hidesources
                Smith |> Option.iter layout.set_smith
                Showlegend |> Option.iter layout.set_showlegend
                Hovermode |> Option.iter (StyleOption.Hovermode.toString >> layout.set_hovermode)
                Dragmode |> Option.iter (StyleOption.Dragmode.toString >> layout.set_dragmode)
            
                Geo |> Option.iter layout.set_geo
            
                Annotations |> Option.iter layout.set_annotations
            

                Direction |> Option.iter (StyleOption.Direction.toString >> layout.set_direction)
                Orientation |> Option.iter layout.set_orientation
                Barmode |> Option.iter (StyleOption.Barmode.toString >> layout.set_barmode)
                Bargap |> Option.iter layout.set_bargap

                // Update
                Font        |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.font @>)
                Titlefont   |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.titlefont @>)
                Margin      |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.margin @>)
                xAxis       |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.xaxis @>)
                xAxis2      |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.xaxis2 @>)
                yAxis       |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.yaxis @>)
                yAxis2      |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.yaxis2 @>)
                Legend      |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.legend @>)
                Radialaxis  |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.radialaxis @>)
                Angularaxis |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.angularaxis @>)
                Scene       |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.scene @>)
                Shapes      |> Option.iter (updatePropertyValueAndIgnore layout <@ layout.shapes @>)

                layout
            )


    // Applies the styles to Margin()
    static member Margin
        (
            ?Left,
            ?Right,
            ?Top,
            ?Bottom,
            ?Pad,
            ?Autoexpand
        ) =
            (fun (margin:('T :> Margin)) -> 
                Left   |> Option.iter margin.set_l
                Right  |> Option.iter margin.set_r
                Top    |> Option.iter margin.set_t
                Bottom |> Option.iter margin.set_b

                Pad        |> Option.iter margin.set_pad
                Autoexpand |> Option.iter margin.set_autoexpand

                margin
            )

    // Applies the styles to LinearAxis()
    // TODO: obj to fun
    static member Axis
        (
            ?AxisType, 
            ?Title,            
            ?Titlefont:FontOptions,                             
            ?Autorange,        
            ?Rangemode,        
            ?Range,            
            ?Fixedrange,       
            ?Tickmode,         
            ?nTicks,           
            ?Tick0,            
            ?dTick,            
            ?Tickvals,         
            ?Ticktext,         
            ?Ticks,            
            ?Mirror,           
            ?Ticklen,          
            ?Tickwidth,        
            ?Tickcolor,        
            ?Showticklabels,   
            ?Tickfont:FontOptions,         
            ?Tickangle,        
            ?Tickprefix,       
            ?Showtickprefix,   
            ?Ticksuffix,       
            ?Showticksuffix,   
            ?Showexponent,     
            ?Exponentformat,   
            ?Tickformat,       
            ?Hoverformat,      
            ?Showline,         
            ?Linecolor,        
            ?Linewidth,        
            ?Showgrid,         
            ?Gridcolor,        
            ?Gridwidth,        
            ?Zeroline,         
            ?Zerolinecolor,    
            ?Zerolinewidth,    
            ?Anchor,           
            ?Side,             
            ?Overlaying,       
            ?Domain,           
            ?Position,         
            ?IsSubplotObj,    
            ?Tickvalssrc,      
            ?Ticktextsrc,      
            ?Showspikes,       
            ?Spikesides,       
            ?Spikethickness,   
            ?Spikecolor,       
            ?Showbackground,   
            ?Backgroundcolor,  
            ?Showaxeslabels       
        ) =
            (fun (axis:('T :> AxisObjects.LinearAxis)) -> 
                AxisType        |> Option.iter (StyleOption.AxisType.convert >> axis.set_type)
                Title           |> Option.iter axis.set_title                 
                
                Autorange       |> Option.iter (StyleOption.AutoRange.convert >> axis.set_autorange)
                Rangemode       |> Option.iter (StyleOption.RangeMode.convert >> axis.set_rangemode)
                Range           |> Option.iter (StyleOption.RangeValues.convert >> axis.set_range)       
                Fixedrange      |> Option.iter axis.set_fixedrange      
                Tickmode        |> Option.iter (StyleOption.TickMode.convert >>  axis.set_tickmode)
                nTicks          |> Option.iter axis.set_nticks          
                Tick0           |> Option.iter axis.set_tick0           
                dTick           |> Option.iter axis.set_dtick           
                Tickvals        |> Option.iter axis.set_tickvals        
                Ticktext        |> Option.iter axis.set_ticktext        
                Ticks           |> Option.iter (StyleOption.TickOptions.convert >> axis.set_ticks)
                Mirror          |> Option.iter (StyleOption.Mirror.convert >> axis.set_mirror)
                Ticklen         |> Option.iter axis.set_ticklen         
                Tickwidth       |> Option.iter axis.set_tickwidth       
                Tickcolor       |> Option.iter axis.set_tickcolor       
                Showticklabels  |> Option.iter axis.set_showticklabels             

                Tickangle       |> Option.iter axis.set_tickangle       
                Tickprefix      |> Option.iter axis.set_tickprefix      
                Showtickprefix  |> Option.iter (StyleOption.ShowTickOption.convert >> axis.set_showtickprefix)
                Ticksuffix      |> Option.iter axis.set_ticksuffix    
                Showticksuffix  |> Option.iter (StyleOption.ShowTickOption.convert >> axis.set_showticksuffix)
                Showexponent    |> Option.iter (StyleOption.ShowExponent.convert >> axis.set_showexponent)
                Exponentformat  |> Option.iter (StyleOption.ExponentFormat.convert >> axis.set_exponentformat)  
                Tickformat      |> Option.iter axis.set_tickformat      
                Hoverformat     |> Option.iter axis.set_hoverformat     
                Showline        |> Option.iter axis.set_showline        
                Linecolor       |> Option.iter axis.set_linecolor       
                Linewidth       |> Option.iter axis.set_linewidth       
                Showgrid        |> Option.iter axis.set_showgrid        
                Gridcolor       |> Option.iter axis.set_gridcolor       
                Gridwidth       |> Option.iter axis.set_gridwidth       
                Zeroline        |> Option.iter axis.set_zeroline        
                Zerolinecolor   |> Option.iter axis.set_zerolinecolor   
                Zerolinewidth   |> Option.iter axis.set_zerolinewidth   
                Anchor          |> Option.iter axis.set_anchor          
                Side            |> Option.iter (StyleOption.Side.convert >> axis.set_side)
                Overlaying      |> Option.iter axis.set_overlaying      
                Domain          |> Option.iter (StyleOption.RangeValues.convert >> axis.set_domain)               
                Position        |> Option.iter axis.set_position        
                IsSubplotObj    |> Option.iter axis.set__isSubplotObj    
                Tickvalssrc     |> Option.iter axis.set_tickvalssrc     
                Ticktextsrc     |> Option.iter axis.set_ticktextsrc     
                Showspikes      |> Option.iter axis.set_showspikes      
                Spikesides      |> Option.iter axis.set_spikesides      
                Spikethickness  |> Option.iter axis.set_spikethickness  
                Spikecolor      |> Option.iter axis.set_spikecolor      
                Showbackground  |> Option.iter axis.set_showbackground  
                Backgroundcolor |> Option.iter axis.set_backgroundcolor 
                Showaxeslabels  |> Option.iter axis.set_showaxeslabels     

                //Update
                Titlefont       |> Option.iter (updatePropertyValueAndIgnore axis <@ axis.titlefont @>) 
                Tickfont        |> Option.iter (updatePropertyValueAndIgnore axis <@ axis.tickfont @>)

                axis
            )


    static member Colorbar
        (   
            ?Thicknessmode,  
            ?Thickness,      
            ?Lenmode,        
            ?Len,            
            ?X,              
            ?Xanchor,
            ?Xpad,           
            ?Y,              
            ?Yanchor,        
            ?Ypad,           
            ?Outlinecolor,   
            ?Outlinewidth,   
            ?Bordercolor,    
            ?Borderwidth,    
            ?Bgcolor,        
            ?Tickmode,       
            ?nTicks,         
            ?Tick0,          
            ?dTick,          
            ?Tickvals,       
            ?Ticktext,       
            ?Ticks,          
            ?Ticklen,        
            ?Tickwidth,      
            ?Tickcolor,      
            ?Showticklabels, 
            ?Tickfont,       
            ?Tickangle,      
            ?Tickformat,     
            ?Tickprefix,     
            ?Showtickprefix, 
            ?Ticksuffix,     
            ?Showticksuffix, 
            ?Exponentformat, 
            ?Showexponent,   
            ?Title,          
            ?Titlefont,      
            ?Titleside,      
            ?Tickvalssrc,
            ?Ticktextsrc    

        ) =
            
            (fun (colorbar:('T :> Colorbar)) ->            
                Thicknessmode  |> Option.iter colorbar.set_thicknessmode  
                Thickness      |> Option.iter colorbar.set_thickness      
                Lenmode        |> Option.iter colorbar.set_lenmode        
                Len            |> Option.iter colorbar.set_len            
                X              |> Option.iter colorbar.set_x              
                Xanchor        |> Option.iter colorbar.set_xanchor        
                Xpad           |> Option.iter colorbar.set_xpad           
                Y              |> Option.iter colorbar.set_y              
                Yanchor        |> Option.iter colorbar.set_yanchor        
                Ypad           |> Option.iter colorbar.set_ypad           
                Outlinecolor   |> Option.iter colorbar.set_outlinecolor   
                Outlinewidth   |> Option.iter colorbar.set_outlinewidth   
                Bordercolor    |> Option.iter colorbar.set_bordercolor    
                Borderwidth    |> Option.iter colorbar.set_borderwidth    
                Bgcolor        |> Option.iter colorbar.set_bgcolor        
                Tickmode       |> Option.iter colorbar.set_tickmode       
                nTicks         |> Option.iter colorbar.set_nticks         
                Tick0          |> Option.iter colorbar.set_tick0          
                dTick          |> Option.iter colorbar.set_dtick          
                Tickvals       |> Option.iter colorbar.set_tickvals       
                Ticktext       |> Option.iter colorbar.set_ticktext       
                Ticks          |> Option.iter colorbar.set_ticks          
                Ticklen        |> Option.iter colorbar.set_ticklen        
                Tickwidth      |> Option.iter colorbar.set_tickwidth      
                Tickcolor      |> Option.iter colorbar.set_tickcolor      
                Showticklabels |> Option.iter colorbar.set_showticklabels 
                Tickfont       |> Option.iter colorbar.set_tickfont       
                Tickangle      |> Option.iter colorbar.set_tickangle      
                Tickformat     |> Option.iter colorbar.set_tickformat     
                Tickprefix     |> Option.iter colorbar.set_tickprefix     
                Showtickprefix |> Option.iter colorbar.set_showtickprefix 
                Ticksuffix     |> Option.iter colorbar.set_ticksuffix     
                Showticksuffix |> Option.iter colorbar.set_showticksuffix 
                Exponentformat |> Option.iter colorbar.set_exponentformat 
                Showexponent   |> Option.iter colorbar.set_showexponent   
                Title          |> Option.iter colorbar.set_title          
                Titlefont      |> Option.iter colorbar.set_titlefont      
                Titleside      |> Option.iter colorbar.set_titleside      
                Tickvalssrc    |> Option.iter colorbar.set_tickvalssrc    
                Ticktextsrc    |> Option.iter colorbar.set_ticktextsrc         

                colorbar
            )


    // Applies the styles to Bins()
    static member Bins
        (
            ?StartBins:float,
            ?EndBins:float,
            ?Size
        ) =
            
            (fun (bins:('T :> Bins)) -> 
                StartBins |> Option.iter bins.set_start
                EndBins   |> Option.iter bins.set_end
                Size      |> Option.iter bins.set_size
           
                bins
            )

    // Applies the styles to Pie()
    static member Pie
        (                
            ?TraceOptions:TraceOptions<_>,
            ?Values,
            ?Labels,
            ?Opacity,
            ?Label0,
            ?dLabel,   
            ?Marker,
            ?Text,
            ?Scalegroup,
            ?Textinfo,
            ?Textposition: StyleOption.TextPosition,
            ?Textfont: FontOptions,                    
            ?Insidetextfont: FontOptions,
            ?Outsidetextfont: FontOptions,
            ?Domain, // TODO
            ?Hole: float,
            ?Sort: bool,
            ?Direction, // TODO
            ?Rotation: float,
            ?Pull: float,
            ?Labelssrc: string,
            ?Valuessrc: string,
            ?Textsrc: string,
            ?Textpositionsrc,
            ?Pullsrc

        ) =
            (fun (pie:('T :> Pie)) -> 

                Values          |> Option.iter pie.set_values
                Labels          |> Option.iter pie.set_labels
                Opacity         |> Option.iter pie.set_opacity
                Label0          |> Option.iter pie.set_label0
                dLabel          |> Option.iter pie.set_dlabel
                Text            |> Option.iter pie.set_text
                Scalegroup      |> Option.iter pie.set_scalegroup
                Textinfo        |> Option.iter pie.set_textinfo
                Textposition    |> Option.iter (StyleOption.TextPosition.toString >> pie.set_textposition)                
                                
                Domain          |> Option.iter pie.set_domain         
                Hole            |> Option.iter pie.set_hole           
                Sort            |> Option.iter pie.set_sort           
                Direction       |> Option.iter pie.set_direction      
                Rotation        |> Option.iter pie.set_rotation       
                Pull            |> Option.iter pie.set_pull           
                Labelssrc       |> Option.iter pie.set_labelssrc      
                Valuessrc       |> Option.iter pie.set_valuessrc      
                Textsrc         |> Option.iter pie.set_textsrc        
                Textpositionsrc |> Option.iter pie.set_textpositionsrc
                Pullsrc         |> Option.iter pie.set_pullsrc        
                
                // Update
                Marker          |> Option.iter (updatePropertyValueAndIgnore pie <@ pie.marker          @>)
                Textfont        |> Option.iter (updatePropertyValueAndIgnore pie <@ pie.textfont        @>)
                Insidetextfont  |> Option.iter (updatePropertyValueAndIgnore pie <@ pie.insidetextfont  @>)
                Outsidetextfont |> Option.iter (updatePropertyValueAndIgnore pie <@ pie.outsidetextfont @>)
                    
                // out ->
                pie |> (optApply TraceOptions) 
            ) 


    // Applies the styles to Box()
    static member BoxPlot
        (                
            ?TraceOptions:TraceOptions<_>,
            ?Y,           
            ?X,           
            ?X0,          
            ?Y0,          
            ?Whiskerwidth,
            ?Boxpoints,   
            ?Boxmean,     
            ?Jitter,      
            ?Pointpos,    
            ?Orientation, 
            ?Line        : LineOptions,                         
            ?Marker       : MarkerOptions,
            ?Fillcolor,   
            ?xAxis,       
            ?yAxis,       
            ?Ysrc,        
            ?Xsrc        

        ) =
            (fun (boxPlot:('T :> Box)) -> 

                Y            |> Option.iter boxPlot.set_y           
                X            |> Option.iter boxPlot.set_x           
                X0           |> Option.iter boxPlot.set_x0          
                Y0           |> Option.iter boxPlot.set_y0          
                Whiskerwidth |> Option.iter boxPlot.set_whiskerwidth
                Boxpoints    |> Option.iter (StyleOption.Boxpoints.convert >> boxPlot.set_boxpoints)  
                Boxmean      |> Option.iter (StyleOption.BoxMean.convert >> boxPlot.set_boxmean)    
                Jitter       |> Option.iter boxPlot.set_jitter      
                Pointpos     |> Option.iter boxPlot.set_pointpos    
                Orientation  |> Option.iter (StyleOption.Orientation.convert >> boxPlot.set_orientation)
                Fillcolor    |> Option.iter boxPlot.set_fillcolor   
                xAxis        |> Option.iter boxPlot.set_xaxis       
                yAxis        |> Option.iter boxPlot.set_yaxis       
                Ysrc         |> Option.iter boxPlot.set_ysrc        
                Xsrc         |> Option.iter boxPlot.set_xsrc        
                // Update
                Line         |> Option.iter (updatePropertyValueAndIgnore boxPlot <@ boxPlot.line   @>)
                Marker       |> Option.iter (updatePropertyValueAndIgnore boxPlot <@ boxPlot.marker @>)
                
                // out ->
                boxPlot |> (optApply TraceOptions) 
            ) 


    // Applies the styles to Heatmap()
    static member HeatMap
        (                
            ?TraceOptions:TraceOptions<_>,
            ?Z : seq<#seq<#IConvertible>>,
            ?X : seq<#IConvertible>,
            ?X0             ,
            ?dX             ,
            ?Y : seq<#IConvertible>,
            ?Y0             ,
            ?dY             ,
            ?Text           ,
            ?Transpose      ,
            ?xType          ,
            ?yType          ,
            ?zAuto          ,
            ?zMin           ,
            ?zMax           ,
            ?Colorscale     ,
            ?Autocolorscale ,
            ?Reversescale   ,
            ?Showscale      ,
            ?zSmooth        ,
            ?Connectgaps    ,
            ?Colorbar       ,
            ?xAxis          ,
            ?yAxis          ,
            ?Zsrc           ,
            ?Xsrc           ,
            ?Ysrc           ,
            ?Textsrc        



        ) =
            (fun (heatMap:('T :> Heatmap)) -> 

                Z              |> Option.iter heatMap.set_z              
                X              |> Option.iter heatMap.set_x              
                X0             |> Option.iter heatMap.set_x0             
                dX             |> Option.iter heatMap.set_dx             
                Y              |> Option.iter heatMap.set_y             
                Y0             |> Option.iter heatMap.set_y0            
                dY             |> Option.iter heatMap.set_dy            
                Text           |> Option.iter heatMap.set_text          
                Transpose      |> Option.iter heatMap.set_transpose     
                xType          |> Option.iter heatMap.set_xtype         
                yType          |> Option.iter heatMap.set_ytype         
                zAuto          |> Option.iter heatMap.set_zauto         
                zMin           |> Option.iter heatMap.set_zmin          
                zMax           |> Option.iter heatMap.set_zmax          
                Colorscale     |> Option.iter (StyleOption.ColorScale.convert >> heatMap.set_colorscale)  
                Autocolorscale |> Option.iter heatMap.set_autocolorscale
                Reversescale   |> Option.iter heatMap.set_reversescale  
                Showscale      |> Option.iter heatMap.set_showscale     
                zSmooth        |> Option.iter (StyleOption.SmoothAlg.convert >> heatMap.set_zsmooth)     
                Connectgaps    |> Option.iter heatMap.set_connectgaps   
                Colorbar       |> Option.iter heatMap.set_colorbar      
                xAxis          |> Option.iter heatMap.set_xaxis         
                yAxis          |> Option.iter heatMap.set_yaxis         
                Zsrc           |> Option.iter heatMap.set_zsrc          
                Xsrc           |> Option.iter heatMap.set_xsrc          
                Ysrc           |> Option.iter heatMap.set_ysrc          
                Textsrc        |> Option.iter heatMap.set_textsrc       
                               
                // out ->
                heatMap |> (optApply TraceOptions) 
            ) 


    // Applies the styles to Histogram2d()
    static member Histogram2d
        (                
            ?TraceOptions:TraceOptions<_>,
            ?Y,           
            ?X,           
            ?X0,          
            ?Y0,          
            ?Whiskerwidth,
            ?Boxpoints,   
            ?Boxmean,     
            ?Jitter,      
            ?Pointpos,    
            ?Orientation, 
            ?Line        : LineOptions,                         
            ?Marker       : MarkerOptions,
            ?Fillcolor,   
            ?xAxis,       
            ?yAxis,       
            ?Ysrc,        
            ?Xsrc        

        ) =
            (fun (boxPlot:('T :> Box)) -> 

                Y            |> Option.iter boxPlot.set_y           
                X            |> Option.iter boxPlot.set_x           
                X0           |> Option.iter boxPlot.set_x0          
                Y0           |> Option.iter boxPlot.set_y0          
                Whiskerwidth |> Option.iter boxPlot.set_whiskerwidth
                Boxpoints    |> Option.iter (StyleOption.Boxpoints.convert >> boxPlot.set_boxpoints)  
                Boxmean      |> Option.iter (StyleOption.BoxMean.convert >> boxPlot.set_boxmean)    
                Jitter       |> Option.iter boxPlot.set_jitter      
                Pointpos     |> Option.iter boxPlot.set_pointpos    
                Orientation  |> Option.iter (StyleOption.Orientation.convert >> boxPlot.set_orientation)
                Fillcolor    |> Option.iter boxPlot.set_fillcolor   
                xAxis        |> Option.iter boxPlot.set_xaxis       
                yAxis        |> Option.iter boxPlot.set_yaxis       
                Ysrc         |> Option.iter boxPlot.set_ysrc        
                Xsrc         |> Option.iter boxPlot.set_xsrc        
                // Update
                Line         |> Option.iter (updatePropertyValueAndIgnore boxPlot <@ boxPlot.line   @>)
                Marker       |> Option.iter (updatePropertyValueAndIgnore boxPlot <@ boxPlot.marker @>)
                
                // out ->
                boxPlot |> (optApply TraceOptions) 
            ) 














